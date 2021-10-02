using GuardianPlugin;
using RoR2;
using RoR2.Skills;
using System.Linq;
using UnityEngine;

namespace Guardian.Modules.Guardian
{
    public class VirtueController : MonoBehaviour
    {
        private CharacterBody characterBody;
        private HealthComponent healthComponent;
        private bool scriptEnabled = false;

        // Selection
        public static SkillDef passiveCore;
        public static SkillDef passiveDragonhunter;
        public static SkillDef passiveFirebrand;
        public static GenericSkill selectedPassive;

        // Justice
        public int justiceCount;
        private bool justiceUsed = false;
        private int maxJusticeCount = 1;
        private float justiceCooldown = 10f;

        // Resolve
        private bool resolveUsed = false;
        private float resolveTimer = 2f;
        private float resolveCooldown = 15f;

        // Courage
        private bool courageUsed = false;
        private float courageTimer = 10f;
        private float courageCooldown = 20f;
        private int courageToApply = 3;

        // Visible
        public bool _justiceUsed;
        public bool _resolveUsed;
        public bool _courageUsed;
        public float _justiceCooldown;
        public float _resolveCooldown;
        public float _courageCooldown;

        private void Awake()
        {
            if (GetComponent<CharacterBody>().baseNameToken.Contains("GUARDIAN"))
            {
                characterBody = GetComponent<CharacterBody>();

                justiceCount = 0;
                ApplyJustice(1);
                ApplyCourage();

                scriptEnabled = true;
            }
        }

        private void FixedUpdate()
        {
            if (scriptEnabled)
            {
                PassiveJustice();
                PassiveResolve();
                PassiveCourage();
            }
        }

        private void Update()
        {
            if (scriptEnabled)
            {
                Controls();

                _justiceUsed = justiceUsed;
                _resolveUsed = resolveUsed;
                _courageUsed = courageUsed;

                _justiceCooldown = justiceCooldown;
                _resolveCooldown = resolveCooldown;
                _courageCooldown = courageCooldown;
            }
        }

        private void Controls()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !justiceUsed)
            {
                ActiveJustice();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !resolveUsed)
            {
                ActiveResolve();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && !courageUsed)
            {
                ActiveCourage();
            }
        }

        public void SetupVirtues(SkillDef[] passives, GenericSkill passive)
        {
            Debug.Log(passives[0].skillNameToken + " : " + passives[1].skillNameToken + " : " + passives[2].skillNameToken + " : " + passive.skillNameToken);

            passiveCore = passives[0];
            passiveDragonhunter = passives[1];
            passiveFirebrand = passives[2];
            selectedPassive = passive;        
        }

        #region Justice
        private void ActiveJustice()
        {
            if (!justiceUsed)
            {
                justiceUsed = true;

                ApplyJustice(5);
            }
        }

        public void PassiveJustice()
        {
            if (justiceUsed)
            {
                justiceCooldown -= 1f * Time.fixedDeltaTime;

                if (justiceCooldown <= 0f)
                {
                    justiceCooldown = 10f;
                    justiceCount = 0;
                    justiceUsed = false;
                }
            }
        }

        public void ProgressJustice()
        {
            if (!justiceUsed)
            {
                if (justiceCount + 1 > maxJusticeCount)
                {
                    ApplyJustice(1);
                    justiceCount = 0;
                }
                else
                {
                    justiceCount++;
                }
            }
        }

        private void ApplyJustice(int stacks)
        {
            // 757469347: ror2_item_fireballsOnHit_shoot_01
            Util.PlaySound("ror2_item_fireballsOnHit_shoot_01", base.gameObject);

            for (int i = 0; i < stacks; i++)
            {
                characterBody.AddBuff(GuardianPlugin.Modules.Buffs.justiceBuff);
            }       
        }
        #endregion

        #region Resolve
        private void ActiveResolve()
        {
            if (!resolveUsed)
            {
                resolveUsed = true;

                // 1065927408: ror2_item_proc_TPhealingNova_01
                Util.PlaySound("ror2_item_proc_TPhealingNova_01", base.gameObject);

                ApplyResolve(10f, true);
            }
        }

        private void PassiveResolve()
        {
            if (!resolveUsed)
            {
                resolveTimer -= 1f * Time.fixedDeltaTime;

                if (resolveTimer <= 0f)
                {
                    ApplyResolve(2f, false);
                    resolveTimer = 2f;
                }
            }
            else
            {
                resolveCooldown -= 1f * Time.fixedDeltaTime;

                if (resolveCooldown <= 0f)
                {
                    resolveCooldown = 0f;
                    resolveTimer = 2f;
                    resolveUsed = false;
                }
            }
        }

        private void ApplyResolve(float percent, bool area)
        {
            percent /= 100;

            if (area)
            {
                for (int i = 0; i < CharacterMaster.readOnlyInstancesList.Count; i++)
                {
                    if (Vector3.Distance(base.gameObject.transform.position, CharacterMaster.readOnlyInstancesList[i].transform.position) <= 60)
                    {
                        // 178055884: ror2_item_proc_TPhealingNova_hitPlayer_01
                        Util.PlaySound("ror2_item_proc_TPhealingNova_hitPlayer_01", CharacterMaster.readOnlyInstancesList[i].gameObject);
                        CharacterMaster.readOnlyInstancesList[i].GetComponent<HealthComponent>().Heal(CharacterMaster.readOnlyInstancesList[i].GetComponent<CharacterBody>().maxHealth * percent, new ProcChainMask(), true);
                    }
                }
            }
            else
            {
                GetComponent<HealthComponent>().Heal(GetComponent<CharacterBody>().maxHealth * percent, new ProcChainMask(), true);
            }
        }
        #endregion

        #region Courage
        private void ActiveCourage()
        {
            if (!courageUsed)
            {
                courageUsed = true;

                ApplyCourage();

                courageTimer = 1f;
            }
        }

        private void PassiveCourage()
        {
            if (!courageUsed)
            {
                courageTimer -= 1f * Time.fixedDeltaTime;

                if (courageTimer <= 0f)
                {
                    ApplyCourage();
                    courageTimer = 10f;
                }
            }
            else
            {
                if (courageToApply > 0)
                {
                    courageTimer -= 1f * Time.fixedDeltaTime;

                    if (courageTimer <= 0f)
                    {
                        ApplyCourage();
                        courageTimer = 1f;
                        courageToApply--;
                    }
                }

                courageCooldown -= 1f * Time.fixedDeltaTime;

                if (courageCooldown <= 0f)
                {
                    courageCooldown = 20f;
                    courageTimer = 0f;
                    courageToApply = 3;
                    courageUsed = false;
                }
            }
        }

        private void ApplyCourage()
        {
            characterBody.AddTimedBuff(GuardianPlugin.Modules.Buffs.aegisBuff, 10f);
        }
        #endregion
    }
}
