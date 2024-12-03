using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; // specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;

        public float health = 100;
        public float hunger = 100;
        public float thirst = 100;

        public float deathRate;
        public float thirstRate;
        public float hungerRate;

        public Slider Hbar;
        public Slider Tbar;
        public Slider healthBar;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        void Update()
        {
            healthBar.value = health;
            Hbar.value = hunger;
            Tbar.value = thirst;

            hunger -= hungerRate * Time.deltaTime;
            thirst -= thirstRate * Time.deltaTime;

            if (health <= 0)
            {
                health = 0;
                m_Animator.SetTrigger("dead");
                gameObject.GetComponent<ThirdPersonUserControl>().enabled = false;
            }
            if (hunger <= 0 || thirst <= 0)
            {
                health -= deathRate * Time.deltaTime;
            }
            if (hunger <= 0)
            {
                hunger = 0;
            }
            if (thirst <= 0)
            {
                thirst = 0;
            }
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();

            if (m_IsGrounded)
            {
                HandleGroundedMovement(crouch, jump);
            }
            else
            {
                HandleAirborneMovement();
            }

            ScaleCapsuleForCrouching(crouch);
            PreventStandingInLowHeadroom();

            UpdateAnimator(move);
        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (m_IsGrounded && crouch)
            {
                if (m_Crouching) return;
                m_Capsule.height /= 2f;
                m_Capsule.center /= 2f;
                m_Crouching = true;
            }
            else
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                    return;
                }
                m_Capsule.height = m_CapsuleHeight;
                m_Capsule.center = m_CapsuleCenter;
                m_Crouching = false;
            }
        }

        void PreventStandingInLowHeadroom()
        {
            if (!m_Crouching)
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                }
            }
        }

        void UpdateAnimator(Vector3 move)
        {
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("Crouch", m_Crouching);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            if (!m_IsGrounded)
            {
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
            }

            float runCycle = Mathf.Repeat(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
            float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
            if (m_IsGrounded)
            {
                m_Animator.SetFloat("JumpLeg", jumpLeg);
            }

            if (m_IsGrounded && move.magnitude > 0)
            {
                m_Animator.speed = m_AnimSpeedMultiplier;
            }
            else
            {
                m_Animator.speed = 1;
            }
        }

        void HandleAirborneMovement()
        {
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }

        void HandleGroundedMovement(bool crouch, bool jump)
        {
            if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                m_IsGrounded = false;
                m_Animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }

        public void OnAnimatorMove()
        {
            if (m_IsGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
        }

        /*
        ==================== TUTORIAL FOR DEVELOPERS ====================
        This script is used for controlling a third-person character in Unity.
        It includes several key features, including:

        1. Movement Control:
           - The `Move` function takes care of handling user inputs and moves the character accordingly.
           - The character can move forward, turn, crouch, and jump.

        2. Animator Integration:
           - The `UpdateAnimator` function is used to sync animation states with the character's movement.
           - Various animator parameters are updated to reflect whether the character is moving, jumping, or crouching.

        3. Crouching Logic:
           - The `ScaleCapsuleForCrouching` function adjusts the capsule collider size when the character crouches.
           - `PreventStandingInLowHeadroom` ensures that the character cannot stand up if there isn't enough space.

        4. Ground Check:
           - `CheckGroundStatus` uses a raycast to check if the character is grounded.
           - This helps to determine if the character is on the ground or airborne.

        5. Handling Airborne and Grounded Movement:
           - `HandleAirborneMovement` applies extra gravity to make the jump feel more realistic.
           - `HandleGroundedMovement` handles jumps when the character is grounded.

        6. Rotation Control:
           - `ApplyExtraTurnRotation` helps to rotate the character smoothly based on input.

        7. Rigidbody Settings:
           - The Rigidbody is set up to constrain rotations along the x, y, and z axes.
           - This helps in keeping the character upright.

        8. Health, Hunger, and Thirst Mechanics:
           - The script also includes simple health, hunger, and thirst management.
           - `Update` function reduces hunger and thirst over time.
           - If hunger or thirst reaches zero, the health decreases gradually.
           - Sliders are used to visually represent health, hunger, and thirst values.

        HOW TO USE THIS SCRIPT:
        - Attach this script to a character GameObject that has a Rigidbody, CapsuleCollider, and Animator.
        - Make sure to configure the Animator with the appropriate parameters: "Forward", "Turn", "Crouch", "OnGround", "Jump", and "JumpLeg".
        - Set up the `healthBar`, `Hbar`, and `Tbar` sliders in the Unity Editor to represent health, hunger, and thirst.

        NOTES:
        - Adjust the serialized fields (e.g., `m_JumpPower`, `m_GravityMultiplier`) to tweak the character's movement.
        - Ensure the character's Animator Controller has the proper animations and transitions to support jumping, crouching, and moving.
        =================================================================
        */
    }
}
